import { getToken } from "./authManager";

 const apiUrl = "/api/team";

 export const getAllTeams = () => {
     return getToken().then((token) => {
         return fetch(apiUrl, {
             method: "GET",
             headers: {
                 Authorization: `Bearer ${token}`
             }
         }).then(res => {
             if(res.ok){
                 return res.json();
             }else {
                 throw new Error("An unknown error occurred while trying to get all teams.");
             }
         })
     })
 }

 export const getTeamById = (id) => {
     return getToken().then(token => {
         return fetch(`${apiUrl}/${id}`, {
             method: "GET",
             headers: {
                 Authorization: `Bearer ${token}`
             }
         }).then(res => {
             if(res.ok){
                 return res.json();
             } else {
                 throw new Error("ERROR GETTING TEAM BY ID");
             }
         })
     })
 }

 export const addTeam = (team) => {
     return getToken().then((token) => {
         return fetch(apiUrl, {
             method: "POST",
             headers: {
                 Authorization: `Bearer ${token}`,
                 "Content-Type": "application/json"
             },
             body: JSON.stringify(team)
         }).then(res => {
             if (res.ok) {
                 return res.json();
             } else if (res.status === 401) {
                 throw new Error("Unauthorized");
             } else {
                 throw new Error("An unknown error occurred while trying to save a new team.");
             }
         })
     })
 }

 export const updateTeam = (team) => {
     return getToken().then(token => {
         return fetch(`${apiUrl}/${team.id}`, {
             method: "PUT",
             headers: {
                 Authorization: `Bearer ${token}`,
                 "Content-Type": "application/json",
             },
             body: JSON.stringify(team),
         })
     })
 }

 export const deleteTeam = (id) => {
     return getToken().then((token) => {
         return fetch(`${apiUrl}/${id}`, {
             method: "DELETE",
             headers: {
                 Authorization: `Bearer ${token}`,
                 "Content-Type": "application/json",
             },
         })
     })
 }