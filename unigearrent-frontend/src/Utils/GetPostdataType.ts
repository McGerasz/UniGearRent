import { PostType } from "../Models/PostType";

const GetPostdataType: (obj: object) => PostType = (obj: any) => {
    if(obj["width"] !== undefined && obj["length"] !== undefined){
        return PostType.Trailer;
    }
    else return PostType.Car
}
export default GetPostdataType;