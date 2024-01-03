import { Profile } from "./Profile";
import { RegistrationType } from "./RegistrationType";

export class LessorProfile extends Profile{
    Name: string;
    constructor(username: string, phoneNumber: string, email: string, token: string, name: string){
        super(username, phoneNumber, email, token, RegistrationType.Lessor)
        this.Name = name;
    }
}