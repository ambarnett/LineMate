import React from "react";
import { Card, CardBody, Button } from "reactstrap";

export default function Team({team, handleDelete}){
    return (
        <Card className="m-4">
            <CardBody>
                <strong className="justify-content-center">{team.name}</strong>
                <Button className="btn btn-danger float-end" onClick={() => handleDelete(team.id)}>Delete</Button>
            </CardBody>
        </Card>
    )
}