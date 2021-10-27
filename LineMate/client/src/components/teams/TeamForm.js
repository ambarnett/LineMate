import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import { Button, Form, FormGroup, Label, Input, Alert } from 'reactstrap';
import { addTeam } from "../../modules/teamManager";

export default function TeamForm() {
    const history = useHistory();
    const [teamName, setTeamName] = useState();

    const submitForm = e => {
        e.preventDefault();
        addTeam({ name: teamName })
        .then(() => history.push("/team"))
        .catch((err) => alert(`An error ocurred: ${err.message}`));
    };

    return (
        <Form onSubmit={submitForm}>
            <FormGroup>
                <Label for="teamName">Team Name</Label>
                <Input id="teamName" type="text" onChange={e => setTeamName(e.target.value)} />
            </FormGroup>
            <FormGroup>
                <Button>
                    Save
                </Button>
            </FormGroup>
        </Form>
    )
}