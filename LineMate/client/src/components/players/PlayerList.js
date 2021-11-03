import React, { useEffect, useState} from "react";
import { Button, Table, Container } from "reactstrap";
import { useHistory } from "react-router-dom";
import Player from "./Player";
import { getAllPlayers } from "../../modules/playerManager";

export default function PlayerList() {
    const [players, setPlayers] = useState([]);
    const history = useHistory();

    useEffect(() => {
        getAllPlayers()
        .then(p => setPlayers(p))
    }, [])

    return (
        <Container>
            <br/>
            <Button outline color="primary" onClick={e => {
                e.preventDefault()
                history.push("/player/add")
            }}>Add New Player</Button>
            <br/>
            <br/>
            <Table bordered striped>
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Last Name</th>
                        <th>First Name</th>
                        <th>Current Team</th>
                    </tr>
                </thead>
                {players.map(p =>
                    <Player key={p.id} player={p} />
                )}
            </Table>
        </Container>
    )
}