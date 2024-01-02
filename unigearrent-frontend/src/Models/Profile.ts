export class Profile{
    Username: string;
    PhoneNumber: string;
    Email: string;
    Token: string;
    constructor(username: string, phoneNumber: string, email: string, token: string){
        this.Username = username;
        this.PhoneNumber = phoneNumber;
        this.Email = email;
        this.Token = token;
    }
}