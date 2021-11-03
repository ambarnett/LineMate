import React, { useEffect, useState } from "react";
import { Button, Card, CardHeader, Container, ListGroup } from "reactstrap";
import { useParams, useHistory, Link } from "react-router-dom";
import { getPlayerById, deletePlayer } from "../../modules/playerManager";

export default function PlayerDetail() {
    const [player, setPlayer] = useState({});
    const param = useParams();
    const history = useHistory();

    useEffect(() => {
        getPlayerById(param.id).then(setPlayer);
    }, []);

    const handleDelete = () => {
        if (window.confirm(`Are you sure you want to delete this player? Press OK to confirm.`)) {
            deletePlayer(player.id)
                .then(history.push("/player"))
        }
    }
    return (
        <Container>
            <Link className="btn btn-dark" to={"/player"}>Player List</Link>
            <ListGroup>
                <Card>
                    <CardHeader>{player.firstName} {player.lastName}</CardHeader>
                    <Button className="btn btn-warning" onClick={() => {
                        history.push(`/player/edit/${player.id}`)
                    }}>Edit player info</Button>
                    <Button onClick={handleDelete}>Delete Player</Button>
                </Card>
            </ListGroup>
        </Container>
    )
}