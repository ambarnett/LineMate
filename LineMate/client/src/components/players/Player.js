import React from "react";
import { Card, CardBody, Button, CardTitle, CardGroup, CardText, CardSubtitle } from "reactstrap";
import { Link, useHistory } from "react-router-dom";

export default function Player({ player, handleDelete }) {
    const history = useHistory();

    return (
        <CardGroup className="m-5">
            <Card className="flex-row">
                <CardBody className="h2 align-self-center text-decoration-underline">
                    #{player.jerseyNumber}
                </CardBody>
                <Card className="m-4">
                    <CardBody>
                        <CardTitle className="justify-content-center" tag="h5">
                            {player.lastName}, {player.firstName}
                        </CardTitle>
                        <CardSubtitle>
                            Position: {player.position}
                        </CardSubtitle>
                        <CardSubtitle>
                            Current Team: {player.team?.name}
                        </CardSubtitle>
                        <Link to={`/player/${player.id}`}>Details</Link>
                    </CardBody>
                </Card>
            </Card>
        </CardGroup>
    )
}