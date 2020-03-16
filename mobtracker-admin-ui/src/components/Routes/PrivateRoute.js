import React from "react";
import { Route } from "react-router-dom";
import { useAuth0 } from "contexts/Auth0";

export default function PrivateRoute({ children, ...rest }) {
  const { isLoading, isAuthenticated, loginWithRedirect } = useAuth0();
  return (
    <Route
      {...rest}
      render={() =>
        !isLoading && isAuthenticated ? children : loginWithRedirect()
      }
    />
  );
}
