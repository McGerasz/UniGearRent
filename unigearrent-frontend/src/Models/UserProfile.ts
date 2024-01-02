import { Profile } from "./Profile";

export class UserProfile extends Profile{
    FirstName: string;
    LastName: string;
    constructor(username: string, phoneNumber: string, email: string, token: string, firstName: string, lastName: string){
        super(username, phoneNumber, email, token);
        this.FirstName = firstName;
        this.LastName = lastName;
    }
}