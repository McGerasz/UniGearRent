import { Profile } from "./Profile";

export class LessorProfile extends Profile{
    Name: string;
    constructor(username: string, phoneNumber: string, email: string, token: string, name: string){
        super(username, phoneNumber, email, token)
        this.Name = name;
    }
}