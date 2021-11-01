import React, { useEffect, useState} from "react";
import { Button, Card, CardBody } from "reactstrap";
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
        <>
            <Button outline color="primary" onClick={e => {
                e.preventDefault()
                history.push("/player/add")
            }}>Add New Player</Button>
            <section>
                {players.map(p =>
                    <Player key={p.id} player={p} />
                )}
            </section>
        </>
    )
}