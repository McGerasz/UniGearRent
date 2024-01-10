export class PostCardData{
    Id: number;
    Name: string;
    Location: string;
    PosterId: string;
    Description: string;

    constructor(id: number, name: string, location: string, posterId: string, description: string){
        this.Id = id;
        this.Name = name;
        this.Location = location;
        this.PosterId = posterId;
        this.Description = description;
    }
}