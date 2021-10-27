import React, { useEffect, useState } from "react";
import { Button, Card, CardBody, ListGroup, ListGroupItem } from "reactstrap";
import { useParams, useHistory, Link } from "react-router-dom";
import { getTeamById } from "../../modules/teamManager";

export default function TeamDetail() {
    const [team, setTeam] = useState({});
    const { id } = useParams();
    const history = useHistory();

    useEffect(() => {
        getTeamById(id).then(setTeam);
    }, []);

    return (
        <div className='container'>
            <Link className='btn btn-dark' to={'/team'}>Go Back</Link>
            <div className='row justify-content-center'>
                <div className='col-sm-12 col-lg-6'>
                    <ListGroup>
                        <h3>{team.name}</h3>
                        <ListGroupItem>
                            <Card>
                                Add list of players on the team
                            </Card>
                        </ListGroupItem>
                    </ListGroup>
                    <ListGroup>
                        <ListGroupItem>
                            Show lines and who's on them
                        </ListGroupItem>
                    </ListGroup>
                    <Button className='btn btn-dark float-end' onClick={() => {
                        history.push(`/team/edit/${team.id}`)
                    }}>Edit Team</Button>
                </div>
            </div>
        </div>
    )
}