import React from "react";
import { useAuth0 } from "contexts/Auth0";

export default function Home() {
  const { isLoading, isAuthenticated, getTokenSilently } = useAuth0();

  const testApi = async () => {
    if (!isLoading && isAuthenticated) {
      const URL = "/api/test";
      const TOKEN = await getTokenSilently();
      console.log("TOKEN", TOKEN);
      const AuthStr = "Bearer ".concat(TOKEN);

      fetch(URL, {
        method: "get",
        headers: {
          Authorization: AuthStr
        }
      })
        .then(response => {
          return response.text();
        })
        .then(data => {
          console.log(data);
        });
    }
  };
  return (
    <div>
      Home
      <button onClick={testApi}>Test</button>
    </div>
  );
}
