import React, { useEffect, useState } from "react";
import { useHistory, useParams } from "react-router";
import { Button, Form, FormGroup, Label, Input, Container, Row, Col } from 'reactstrap';
import { addPlayer, getPlayerById, updatePlayer } from "../../modules/playerManager";
import { getAllTeams } from "../../modules/teamManager";

export default function PlayerForm() {
    const history = useHistory();
    const [player, setPlayer] = useState({});
    const [teams, setTeams] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const params = useParams();
    //need to also bring in teams to be able to select the team the player is on if they are currently on a team

    useEffect(() => {
        if (params.id) {
            getPlayerById(params.id)
                .then(p => {
                    setPlayer(p);
                    setIsLoading(false)
                })
        }
    }, [])

    useEffect(() => {
        getAllTeams().then(setTeams)
    }, [])

    const handleInputChange = e => {
        const playerCopy = { ...player }
        playerCopy[e.target.id] = e.target.value
        setPlayer(playerCopy)
    }

    const submitForm = e => {
        e.preventDefault();
        if (params.id) {
            setIsLoading(true)
            updatePlayer(player)
                .then(() => {
                    history.push("/player")
                })
        } else {
            addPlayer(player)
                .then(() => history.push("/player"))
                .catch((err) => alert(`An error ocurred: ${err.message}`));
        }
    };

    return (
        <Container className="mt-4">
            <Form>
                <Col form>
                    <Row md={3}>
                        <FormGroup>
                            <Label for="firstName">First Name</Label>
                            <Input
                                id="firstName"
                                type="text"
                                name="firstName"
                                placeholder="First Name"
                                value={player.firstName}
                                onChange={handleInputChange} />
                        </FormGroup>
                        <FormGroup>
                            <Label for="lastName">Last Name</Label>
                            <Input
                                id="lastName"
                                type="text"
                                name="lastName"
                                placeholder="Last Name"
                                value={player.lastName}
                                onChange={handleInputChange} />
                        </FormGroup>
                    </Row>
                </Col>
                <Row md={3}>
                    <FormGroup>
                        <Label for="position">Position</Label>
                        <Input
                            id="position"
                            name="position"
                            type="select"
                            value={player.position}
                            onChange={handleInputChange}
                        >
                            <option>Select Position</option>
                            <option>Left Wing</option>
                            <option>Center</option>
                            <option>Right Wing</option>
                            <option>Defence</option>
                            <option>Goalie</option>
                        </Input>
                    </FormGroup>
                </Row>
                <Row md={4}>
                    <FormGroup>
                        <Label for="jerseyNumber">Jersey Number</Label>
                        <Input
                            id="jerseyNumber"
                            name="jerseyNumber"
                            type="number"
                            max="99"
                            min="1"
                            value={player.jerseyNumber}
                            onChange={handleInputChange} />
                    </FormGroup>
                </Row>
                <Row md={4}>
                    <FormGroup>
                        <Label for="line">Line</Label>
                        <Input
                            id="line"
                            name="line"
                            type="select"
                            value={player.line}
                            onChange={handleInputChange}
                        >
                            <option>0</option>
                            <option>1</option>
                            <option>2</option>
                            <option>3</option>
                            <option>4</option>
                        </Input>
                    </FormGroup>
                </Row>
                <Row md={2}>
                    <FormGroup>
                        <Label for="teamId">Current Team</Label>
                        <Input
                            id="teamId"
                            name="teamId"
                            type="select"
                            value={player.teamId}
                            onChange={handleInputChange}>
                            <option>Select a Team</option>
                            {teams.map((t) => (<option key={t.id} value={t.id}>{t.name}</option>))}
                        </Input>
                    </FormGroup>
                </Row>
                <Row className="mt-2">
                    <FormGroup>
                        <Button onClick={submitForm}>
                            Save
                        </Button>
                    </FormGroup>
                </Row>
            </Form>
        </Container>
    )
}