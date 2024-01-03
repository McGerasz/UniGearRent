import { Profile } from "./Profile";
import { RegistrationType } from "./RegistrationType";

export class UserProfile extends Profile{
    FirstName: string;
    LastName: string;
    constructor(username: string, phoneNumber: string, email: string, token: string, firstName: string, lastName: string){
        super(username, phoneNumber, email, token, RegistrationType.User);
        this.FirstName = firstName;
        this.LastName = lastName;
    }
}