import React, { useEffect, useState } from "react";
import { Button, ListGroup, ListGroupItem, ListGroupItemHeading, Table } from "reactstrap";
import { useParams, useHistory, Link } from "react-router-dom";
import { getTeamById } from "../../modules/teamManager";

export default function TeamDetail() {
    const [team, setTeam] = useState({});
    const params = useParams();
    const history = useHistory();

    useEffect(() => {
        getTeamById(params.id).then(setTeam);
    }, []);

    // const playersArr = team.players
    // console.log(playersArr)

    // const line1 = playersArr.filter(function (p){
    //     return p.line === 1;
    // })
    return (
        <div className='container'>
            <Link className='btn btn-dark' to={'/team'}>Go Back</Link>
            <div className='row justify-content-center'>
                <div className='col-sm-12 col-lg-6'>
                    <ListGroup>
                        <h3>{team.name}</h3>
                        <ListGroup>
                            <ListGroupItemHeading>Roster:</ListGroupItemHeading>
                            <Table>
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Last Name</th>
                                        <th>First Name</th>
                                        <th>Position</th>
                                    </tr>
                                </thead>
                                {team.players?.map((player) => (
                                    <tbody>
                                        <tr>
                                            <th scope="row">{player.jerseyNumber}</th>
                                            <td>{player.lastName}</td>
                                            <td>{player.firstName}</td>
                                            <td>{player.position}</td>
                                        </tr>
                                    </tbody>
                                ))}
                            </Table>
                        </ListGroup>
                    </ListGroup>
                    <br />
                    <br />
                    <ListGroup>
                        <ListGroupItem>
                            <ListGroupItemHeading>Lines:</ListGroupItemHeading>
                            <Table>
                                <ListGroupItemHeading>Line 1</ListGroupItemHeading>
                                <Table>
                                    {team.players?.map((player) => (
                                        <tbody>
                                            <tr>
                                                <th scope="row">
                                                    <>
                                                        {player.line === 1 ? <>{player.firstName} {player.lastName}</> : ""}
                                                    </>
                                                </th>
                                            </tr>
                                        </tbody>
                                    ))}
                                </Table>
                                <ListGroupItemHeading>Line 2</ListGroupItemHeading>
                                <Table>
                                    {team.players?.map((player) => (
                                        <tbody>
                                            <tr>
                                                <th scope="row">
                                                    {player.line === 2 ? <>{player.firstName} {player.lastName}</> : <></>}
                                                </th>
                                            </tr>
                                        </tbody>
                                    ))}
                                </Table>
                                <ListGroupItemHeading>Line 3</ListGroupItemHeading>
                                <Table>
                                    {team.players?.map((player) => (
                                        <tbody>
                                            <tr>
                                                <th scope="row">
                                                    {player.line === 3 ? <>{player.firstName} {player.lastName}</> : <></>}
                                                </th>
                                            </tr>
                                        </tbody>
                                    ))}
                                </Table>
                                <ListGroupItemHeading>Line 4</ListGroupItemHeading>
                                <Table>
                                    {team.players?.map((player) => (
                                        <tbody>
                                            <tr>
                                                <th scope="row">
                                                    {player.line === 4 ? <>{player.firstName} {player.lastName}</> : <></>}
                                                </th>
                                            </tr>
                                        </tbody>
                                    ))}
                                </Table>
                            </Table>
                        </ListGroupItem>
                    </ListGroup>
                    <Button className='btn btn-dark float-end' onClick={() => {
                        history.push(`/team/edit/${team.id}`)
                    }}>Edit Team</Button>
                </div>
            </div>
        </div >
    )
}