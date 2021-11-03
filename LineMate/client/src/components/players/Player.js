import React from "react";
import { Button } from "reactstrap";
import { useHistory } from "react-router-dom";

export default function Player({ player, handleDelete }) {
    const history = useHistory();

    const playerDetails = () => {
        history.push(`/player/${player.id}`)
    }

    return (
        <>
            <tbody>
                <tr>
                    <th scope="row">
                        {player.jerseyNumber}
                    </th>
                    <td className="justify-content-center" tag="h5">
                        {player.lastName}, {player.firstName}
                    </td>
                    <td>
                        {player.position}
                    </td>
                    <td>
                        {player.team?.name}
                    </td>
                    <td>
                        <Button color="info" onClick={playerDetails}>Details</Button>
                    </td>
                </tr>
            </tbody>
        </>
    )
}