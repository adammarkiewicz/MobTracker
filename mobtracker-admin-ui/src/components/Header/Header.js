import React from "react";
import { useAuth0 } from "contexts/Auth0";
import logo from "assets/logo.svg";
import { Navbar, Nav, Button } from "react-bootstrap";
import { NavLink } from "react-router-dom";

export default function Header() {
  const { isLoading, user, loginWithRedirect, logout } = useAuth0();

  return (
    <header>
      <Navbar bg="light" expand="lg">
        <Navbar.Brand as={NavLink} exact to="/">
          <img
            src={logo}
            alt="MobTracker"
            width="30"
            height="30"
            className="d-inline-block align-top mr-sm-2"
          />
          <span>MobTracker</span>
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="mr-auto">
            {/* {!isLoading && user && ( */}
            <Nav.Link as={NavLink} exact to="/location">
              Location
            </Nav.Link>
            {/* )} */}
          </Nav>
          {/* if there is no user. show the login button */}
          {!isLoading && !user && (
            <Button variant="light" onClick={loginWithRedirect}>
              Login
            </Button>
          )}

          {/* if there is a user. show user name and logout button */}
          {!isLoading && user && (
            <>
              <span>{user.name}</span>
              <Button
                variant="light"
                onClick={() => logout({ returnTo: window.location.origin })}
              >
                Logout
              </Button>
            </>
          )}
        </Navbar.Collapse>
      </Navbar>
    </header>
  );
}
