import React, { useState, useEffect, createContext, useContext } from "react";
import { useAuth0 } from "contexts/Auth0";
import * as signalR from "@aspnet/signalr";

export const ApiContext = createContext();

export const useApi = () => useContext(ApiContext);

export function ApiProvider(props) {
  const {
    isLoading: isAuth0Loading,
    isAuthenticated,
    getTokenSilently
  } = useAuth0();

  const [isLoading, setIsLoading] = useState(true);
  const [connection, setConnection] = useState(null);

  const config = {
    appType: process.env.REACT_APP_APPLICATION_TYPE,
    url: process.env.REACT_APP_API_URL
  };

  const initializeApi = async () => {
    const token = await getTokenSilently();
    const conn = new signalR.HubConnectionBuilder()
      .withUrl(config.url, { accessTokenFactory: () => token })
      .configureLogging(signalR.LogLevel.Information)
      .build();

    await conn.start();
    await conn.invoke("AddToGroup", config.appType);

    setConnection(conn);
    setIsLoading(false);
  };

  useEffect(() => {
    if (!isAuth0Loading && isAuthenticated) {
      initializeApi();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [isAuth0Loading, isAuthenticated]);

  const api = {
    isLoading,
    connection,
    trigerDevicesIntroduction: () =>
      connection.invoke("TrigerDevicesIntroduction")
  };
  const { children } = props;
  return <ApiContext.Provider value={api}>{children}</ApiContext.Provider>;
}
