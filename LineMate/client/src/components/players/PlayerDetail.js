import React, { useEffect, useState } from "react";
import { Button, Card, CardBody, CardHeader, Col, Container, ListGroup, Row } from "reactstrap";
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
        <Container className="mt-4">
            <Link className="btn btn-dark mb-2" to={"/player"}>Back To Player List</Link>
            <ListGroup>
                <Card>
                    <CardHeader>{player.firstName} {player.lastName}</CardHeader>
                    <CardBody>{player.position}</CardBody>
                    <CardHeader></CardHeader>
                    <CardBody>{player.team?.name}</CardBody>
                </Card>
                <Row>
                    <Col className="text-end">
                        <Button className="btn btn-warning" onClick={() => {
                            history.push(`/player/edit/${player.id}`)
                        }}>Edit player info</Button>
                        <Button onClick={handleDelete}>Delete Player</Button>
                    </Col>
                </Row>
            </ListGroup>
        </Container>
    )
}