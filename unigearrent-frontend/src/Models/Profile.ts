import { RegistrationType } from "./RegistrationType";

export class Profile{
    Id: string;
    Username: string;
    PhoneNumber: string;
    Email: string;
    Token: string;
    Type: RegistrationType;
    constructor(id: string, username: string, phoneNumber: string, email: string, token: string, type: RegistrationType){
        this.Id = id;
        this.Username = username;
        this.PhoneNumber = phoneNumber;
        this.Email = email;
        this.Token = token;
        this.Type = type
    }
}