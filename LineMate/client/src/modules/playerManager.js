import { getToken } from "./authManager";

const apiUrl ="/api/player";

export const getAllPlayers = () => {
    return getToken().then((token) => {
        return fetch(apiUrl, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then(res => {
            if(res.ok) {
                return res.json()
            } else {
                throw new Error("ERROR IN GETTING ALL PLAYERS")
            }
        })
    })
}

export const getPlayerById = (id) => {
    return getToken().then(token => {
        return fetch(`${apiUrl}/${id}`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then(res => {
            if (res.ok) {
                return res.json()
            } else {
                throw new Error("ERROR IN GETTING PLAYER BY ID")
            }
        })
    })
}

export const addPlayer = (player) => {
    return getToken().then(token => {
        return fetch(apiUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json",
            },
            body: JSON.stringify(player),
        }).then(res => {
            if(res.ok) {
                return res.json();
            } else if (res.status === 401 ) {
                throw new Error("Unauthorized");
            } else {
                throw new Error("An unknown error occurred while trying to save a new player.");
            }
        })
    })
}

export const deletePlayer = (id) => {
    return getToken().then((token) => {
        return fetch(`${apiUrl}/${id}`, {
            method: "DELETE",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json",
            }
        })
    })
}

export const updatePlayer = (player) => {
    return getToken().then(token => {
        return fetch(`${apiUrl}/${player.id}`, {
            method: "PUT",
            headers: {
                Authorization: `Bearer ${token}`, 
                "Content-Type": "application/json",
            },
            body: JSON.stringify(player),
        })
    })
}