import React from "react";
import "bulma/css/bulma.css";
import { useAuth0 } from "./contexts/auth0-context";
import Header from "./components/Header";
// import * as signalR from "@aspnet/signalr";

function App() {
  const { isLoading, user, loginWithRedirect, logout } = useAuth0();

  return (
    <>
      <Header />

      <div className="hero is-info is-fullheight">
        <div className="hero-body">
          <div className="container has-text-centered">
            {!isLoading && !user && (
              <>
                <h1>Click Below!</h1>
                <button
                  onClick={loginWithRedirect}
                  className="button is-danger"
                >
                  Login
                </button>
              </>
            )}
            {!isLoading && user && (
              <>
                <h1>You are logged in!</h1>
                <p>Hello {user.name}</p>

                {user.picture && <img src={user.picture} alt="My Avatar" />}
                <hr />

                <button
                  onClick={() => logout({ returnTo: window.location.origin })}
                  className="button is-small is-dark"
                >
                  Logout
                </button>
              </>
            )}
          </div>
        </div>
      </div>
    </>
  );

  /* const but = document.createElement("button");
  but.innerText = "Click";
  but.onclick = function changeContent() {
    fetch("/test")
      .then(response => {
        return response.text();
      })
      .then(myText => {
        console.log(myText);

        const div = document.getElementById("root");
        const item = document.createElement("div");
        item.innerText = myText;
        div.appendChild(item);
      });
  };
  document.getElementById("root").appendChild(but);

  const connection = new signalR.HubConnectionBuilder()
    .withUrl("/api")
    .configureLogging(signalR.LogLevel.Information)
    .build();

  connection.on("ReceiveLocation", location => {
    const div = document.createElement("div");
    div.textContent = "Received location: " + location;
    document.getElementById("root").appendChild(div);
  });

  connection.start().then(function() {
    console.log("Connected!!!");

    console.log("Sending location request!");
    connection.invoke("GetLocation");
  });
  return <div id="app" className="App"></div>; */
}

export default App;
