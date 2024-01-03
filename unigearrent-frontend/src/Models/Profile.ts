import { RegistrationType } from "./RegistrationType";

export class Profile{
    Username: string;
    PhoneNumber: string;
    Email: string;
    Token: string;
    Type: RegistrationType;
    constructor(username: string, phoneNumber: string, email: string, token: string, type: RegistrationType){
        this.Username = username;
        this.PhoneNumber = phoneNumber;
        this.Email = email;
        this.Token = token;
        this.Type = type
    }
}