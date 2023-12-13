# Projet de Chat Client-Serveur en C# avec Sockets

Ce projet a été réalisé dans le cadre d'un travail scolaire pour CESI. Il met en œuvre une architecture client-serveur en utilisant la classe Socket de C# pour permettre une communication bidirectionnelle entre un serveur et un client.

## Objectifs

- Mettre en œuvre une architecture client-serveur en C#.
- Comprendre, coder et expliquer l'utilisation de la classe Socket.
- Comprendre, coder et expliquer le flux de données entre le serveur et le client.

## Contenu du Projet

Le projet est divisé en deux parties principales : le serveur (`Server`) et le client (`Client`). Chaque partie a son propre fichier source (server.cs et client.cs) contenant l'implémentation des fonctionnalités nécessaires.

## Instructions d'exécution

1. **Server :** Lancez une instance du projet Server pour démarrer le serveur.

2. **Client :** Lancez une instance du projet Client pour établir une connexion avec le serveur.

## Comment utiliser

- Le serveur attend une connexion cliente et affiche les informations relatives à cette connexion.
- Le client se connecte au serveur et envoie un message au serveur.
- Le serveur reçoit le message du client et l'affiche.
- Le serveur envoie une URL au client, qui lance le navigateur avec l'URL spécifiée.

## Configuration

- Adresse IP du serveur : 127.0.0.1
- Port : 46154 (modifiable dans le code si nécessaire)
