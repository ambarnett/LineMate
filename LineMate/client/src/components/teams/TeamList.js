import React, { useEffect, useState } from "react";
import { Button } from "reactstrap";
import { useHistory } from "react-router-dom";
import Team from "./Team";
import { getAllTeams, deleteTeam } from "../../modules/teamManager";

export default function TeamList() {
    const [teams, setTeams] = useState([]);
    const history = useHistory();

    const getTeamsFromState = () => {
        return getAllTeams()
            .then(teams => setTeams(teams))
    }

    const handleDelete = (teamId) => {
        if (window.confirm(`Are you sure you want to delete this team? Press OK to confirm.`)) {
            deleteTeam(teamId)
                .then(getTeamsFromState())
        } else {
            history.push("/")
        }
    }

    useEffect(() => {
        getTeamsFromState()
    }, [])

    return (
        <>
            <Button outline color="primary" onClick={e => {
                e.preventDefault()
                history.push("/team/add")
            }}>Add Team</Button>
            <section>
                {teams.map(t => <Team key={t.id} team={t} handleDelete={handleDelete} />)}
            </section>
        </>
    );
}