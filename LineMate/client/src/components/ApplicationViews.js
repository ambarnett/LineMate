import React from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import Login from "./Login";
import Register from "./Register";
import Home from "./homePage/Home";
import TeamList from "./teams/TeamList";
import TeamForm from "./teams/TeamForm";
import TeamDetail from "./teams/TeamDetail";

export default function ApplicationViews({ isLoggedIn }) {
    return (
        <main>
            <Switch>
                <Route path="/" exact>
                    {isLoggedIn ? <Home /> : <Redirect to="/login" />}
                </Route>

                <Route path="/team" exact>
                    {isLoggedIn ? <TeamList /> : <Redirect to="/login" />}
                </Route>

                <Route path="/team/add" exact>
                    {isLoggedIn ? <TeamForm /> : <Redirect to="/login" />}
                </Route>

                <Route path='/team/:id' exact>
                    {isLoggedIn ? <TeamDetail /> : <Redirect to='/login' />}
                </Route>

                <Route path="/team/edit/:id" exact>
                    {isLoggedIn ? <TeamForm /> : <Redirect to='/login' />}
                </Route>

                <Route path="/login">
                    <Login />
                </Route>

                <Route path="/register">
                    <Register />
                </Route>
            </Switch>
        </main>
    )
}