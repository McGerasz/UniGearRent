import { JsxElement } from "typescript";
import { PostType } from "../Models/PostType";
import { Button, Card, CardBody, CardHeader, CardText, Col, Container, ListGroup, ListGroupItem, Row } from "react-bootstrap";
import BackendURL from "../Utils/BackendURL";
import { useNavigate } from "react-router-dom";
import { useUserProfile } from "../Utils/UserProfileContextProvider";
import { RegistrationType } from "../Models/RegistrationType";
import { useEffect, useState } from "react";

const PostElement: React.FC<{PostData: any, PostDataType: PostType, MyPost: boolean, PosterName: string}> = (props) => {
    const [isFavourite, setIsFavourite] = useState<boolean>();
    const profile = useUserProfile().userProfile;
    const navigate = useNavigate();
    useEffect(() => {
        if(profile?.Type === RegistrationType.User){
            isFavouriteFetcher()
        }
    }, [])
    const isFavouriteFetcher: () => void = async () => {
        await fetch(BackendURL + "Post/isFavourite?userName=" + profile?.Username + "&Id=" + props.PostData["id"])
        .then(res => res.json()).then(json => setIsFavourite(json));
    }
    const priceVisualizer: () => JSX.Element= () => {
        const priceArr = [props.PostData["hourlyPrice"],props.PostData["dailyPrice"],props.PostData["weeklyPrice"], props.PostData["securityDeposit"]];
        if(!priceArr.some(element => element !== null)) return <></>
        return <ListGroupItem><CardText><Row className="mt-2 mb-2 text-center">{priceArr[0] ? <Col><CardText>Hourly price: {priceArr[0]} HUF</CardText></Col> : <></>}{priceArr[1] ? <Col>Daily price: {priceArr[1]} HUF</Col> : <></>}{priceArr[2] ? <Col>Weekly price: {priceArr[2]} HUF</Col> : <></>}{priceArr[3] ? <Col>Security deposit: {priceArr[3]} HUF</Col> : <></>}</Row></CardText></ListGroupItem>
    }
    const deleteHandler: () => void = async () => {
        if(window.confirm("Are you sure you want to delete this post?") === true){
            await fetch(BackendURL + (props.PostDataType === PostType.Car ? "Car/" : "Trailer/") + props.PostData["id"], {
                method: "DELETE",
                mode: "cors",
                headers: {
                    "Authorization": "Bearer " + profile?.Token
                }});
            alert("Post successfully deleted")
            navigate("/myposts")
        }
        return;
    }
    const editHandler: () => void = () => {
        navigate("/editpost/" + props.PostData["id"])
    }
    const favouriteHandler: () => void = async () => {
        await fetch(BackendURL + "Post/favourite?userName=" + profile?.Username + "&postId=" + props.PostData["id"], {
            method: "POST",
            mode: "cors"
        })
        setIsFavourite(true);
        alert("Post has been added to favourites");
    }
    const removeFavouriteHandler: () => void = async () => {
        await fetch(BackendURL + "Post/favourite?userName=" + profile?.Username + "&postId=" + props.PostData["id"] , {
            method: "DELETE",
            mode: "cors",
            headers: {
                "Authorization": "Bearer " + profile?.Token
            }})
            setIsFavourite(false);
            alert("This post has been removed from your favourites list");
    }
    return(
    <Container>
    <Card className="mt-3" style={{backgroundColor:"#B39377"}}>
        <CardHeader className="text-center"><h1>{props.PostData["name"]}</h1></CardHeader>
        <CardBody>
        <ListGroup>
        <ListGroupItem><CardText className="mt-2 mb-2">{props.PostData["description"]}</CardText></ListGroupItem>
        {priceVisualizer()}
        {props.PostDataType === PostType.Car ? 
        <ListGroupItem><Row className="mt-2 mb-2">
            <Col>Number of seats: {props.PostData["numberOfSeats"]}</Col>
            <Col className="d-flex justify-content-end">Can it be delivered to you: {props.PostData["canDeliverToYou"] ? "Yes" : "No"}</Col>
        </Row></ListGroupItem>
        : props.PostData["width"] === null && props.PostData["length"] === null ? <></> :
         <ListGroupItem><Row className="p-5 text-center">
            {props.PostData["width"] ? <Col>Width: {props.PostData["width"]}</Col> : <></>}
            {props.PostData["length"] ? <Col>Length: {props.PostData["length"]}</Col> : <></>}
         </Row>
         </ListGroupItem>}
         <ListGroupItem>
            <CardText className="mt-2 mb-2">
                This post was created by: {props.PosterName}
            </CardText>
        </ListGroupItem>
         </ListGroup>
         </CardBody>
    </Card>
    {props.MyPost ? <Row className="mt-3">
        <Col className="d-flex justify-content-center">
            <Button onClick={editHandler}>Edit post</Button>
        </Col>
        <Col className="d-flex justify-content-center">
            <Button variant="danger" onClick={deleteHandler}>Delete post</Button>
        </Col>
    </Row> : <></>}
    {
        isFavourite !== undefined ? <Container className="mt-3 d-flex justify-content-end">{isFavourite ? <Button variant="danger" onClick={removeFavouriteHandler}>Remove from favourites</Button>: (<Button onClick={favouriteHandler}>Add to favourites</Button>)}</Container> : <></>
    }
    </Container>)
}
export default PostElement;