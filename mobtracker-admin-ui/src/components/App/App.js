import React from "react";
import Header from "components/Header/Header";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Home from "components/Home/Home";
import Location from "components/Location/Location";
import { Auth0Provider } from "contexts/Auth0";
import { ApiProvider } from "contexts/Api";
//import PrivateRoute from "../Routes/PrivateRoute";

export default function App() {
  return (
    <Auth0Provider>
      <ApiProvider>
        <Router>
          <Header />

          <Switch>
            <Route path="/location">
              <Location />
            </Route>
            <Route path="/">
              <Home />
            </Route>
          </Switch>
        </Router>
      </ApiProvider>
    </Auth0Provider>
  );
}
