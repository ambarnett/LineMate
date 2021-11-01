import React from "react";
import { Card, CardBody, Button } from "reactstrap";
import { Link } from "react-router-dom";

export default function Team({ team, handleDelete }) {
    return (
        <Card className="m-4">
            <CardBody>
                <strong className="justify-content-center">
                    <Link to={`/team/${team.id}`}>
                        {team.name}
                    </Link>
                </strong>
                <Button
                    className="btn btn-danger float-end"
                    onClick={() => handleDelete(team.id)}>
                    Delete
                </Button>
            </CardBody>
        </Card>
    )
}