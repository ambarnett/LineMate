import React, { useEffect, useState } from "react";
import { useHistory, useParams } from "react-router-dom";
import { Button, Form, FormGroup, Label, Input, Alert } from 'reactstrap';
import { addTeam, getTeamById, updateTeam } from "../../modules/teamManager";

export default function TeamForm() {
    const history = useHistory();
    const [team, setTeam] = useState({});
    const [isLoading, setIsLoading] = useState(true);
    const params = useParams();

    useEffect(() => {
        if (params.id) {
            getTeamById(params.id)
                .then(p => {
                    setTeam(p)
                    setIsLoading(false)
                })
        }
    }, [])

    const handleInputChange = e => {
        const teamCopy = { ...team }
        teamCopy[e.target.id] = e.target.value
        setTeam(teamCopy)
    }

    const submitForm = e => {
        e.preventDefault();
        if (params.id) {
            setIsLoading(true)
            updateTeam(team)
                .then(() => {
                    history.push("/team")
                })
        } else {
            addTeam(team)
                .then(() => history.push("/team"))
                .catch((err) => alert(`An error ocurred: ${err.message}`));
        }
    };

    return (
        <Form>
            <FormGroup>
                <Label for="teamName">Team Name</Label>
                <Input id="name" type="text" name="teamName" placeholder="Team Name" value={team.name} onChange={handleInputChange} />
            </FormGroup>
            <FormGroup>
                <Button onClick={submitForm}>
                    Save
                </Button>
            </FormGroup>
        </Form>
    )
}