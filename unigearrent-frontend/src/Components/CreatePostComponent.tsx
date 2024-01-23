import { Button, Col, Container, Form, Row } from "react-bootstrap";
import { PostType } from "../Models/PostType";
import BackendURL from "../Utils/BackendURL";
import { useUserProfile } from "../Utils/UserProfileContextProvider";
import { useNavigate } from "react-router-dom";

const CreatePostComponent: React.FC<{postType: PostType}> = (props) => {
    let canDeliver: boolean = false;
    const profile = useUserProfile().userProfile;
    const navigate = useNavigate();
    const SubmitHandler: (e: React.FormEvent) => void = (e) => {
        e.preventDefault();
        switch(props.postType){
            case PostType.Car.valueOf(): 
            const carHandler: () => void = async () => 
            {
                const target = e.target as typeof e.target & {
                    name: {value: string};
                    location: {value: string};
                    description: {value: string};
                    hourly: {value: number};
                    daily: {value: number};
                    weekly: {value: number};
                    securityDeposit: {value: number}
                    numberOfSeats: {value: number },
                }
                console.log(target.hourly.value.toString())
                const emptiesArray: Array<string> = new Array<string>()
                if(target.name.value === "") emptiesArray.push("name");
                if(target.location.value === "") emptiesArray.push("location");
                if(target.description.value === "") emptiesArray.push("description");
                if(target.numberOfSeats.value.toString() == "") emptiesArray.push("number of seats");
                if(emptiesArray.length > 0){
                    alert("The following fields cannot be empty: " + emptiesArray.join(", "));
                    return;
                }
                const requestData = {
                    name: target.name.value,
                    location: target.location.value,
                    description: target.description.value,
                    hourlyPrice: target.hourly.value.toString() != "" ? target.hourly.value : null,
                    dailyPrice: target.daily.value.toString() != "" ? target.daily.value : null,
                    weeklyPrice: target.weekly.value.toString() != "" ? target.weekly.value : null,
                    securityDeposit: target.securityDeposit.value.toString() != "" ? target.securityDeposit.value : null,
                    numberOfSeats: target.numberOfSeats.value,
                    canDeliverToYou: canDeliver,
                    posterId: ""
                }
                let response = await fetch(BackendURL + "Car/?userName=" + profile?.Username, {
                    method: "POST",
                    mode: "cors",
                    headers: {
                        "Content-type": "application/json",
                        "Authorization": "Bearer " + profile?.Token
                    },
                    body: JSON.stringify(requestData)
                }).then(res => res.json());
            };
                carHandler();
                break;
            case PostType.Trailer.valueOf():
                const trailerHandler: () => void = async () => 
                {
                    const target = e.target as typeof e.target & {
                        name: {value: string};
                        location: {value: string};
                        description: {value: string};
                        hourly: {value: number};
                        daily: {value: number};
                        weekly: {value: number};
                        securityDeposit: {value: number}
                        width: {value: number},
                        length: {value: number}
                    }
                    const emptiesArray: Array<string> = new Array<string>()
                    if(target.name.value === "") emptiesArray.push("name");
                    if(target.location.value === "") emptiesArray.push("location");
                    if(target.description.value === "") emptiesArray.push("description");
                    if(emptiesArray.length > 0){
                        alert("The following fields cannot be empty: " + emptiesArray.join(", "));
                        return;
                    }
                    const requestData = {
                        name: target.name.value,
                        location: target.location.value,
                        description: target.description.value,
                        hourlyPrice: target.hourly.value.toString() != "" ? target.hourly.value : null,
                        dailyPrice: target.daily.value.toString() != "" ? target.daily.value : null,
                        weeklyPrice: target.weekly.value.toString() != "" ? target.weekly.value : null,
                        securityDeposit: target.securityDeposit.value.toString() != "" ? target.securityDeposit.value : null,
                        width: target.width.value.toString() != "" ? target.width.value : null,
                        length: target.length.value.toString() != "" ? target.length.value : null,
                        posterId: ""
                    }
                    let response = await fetch(BackendURL + "Trailer/?userName=" + profile?.Username, {
                        method: "POST",
                        mode: "cors",
                        headers: {
                            "Content-type": "application/json",
                            "Authorization": "Bearer " + profile?.Token
                        },
                        body: JSON.stringify(requestData)
                    }).then(res => res.json());
                };
                trailerHandler()
                break;
        }
        alert("Post successfully created");
        navigate("/myposts");
    }
    
    return (<Container className="w-75"><Form onSubmit={SubmitHandler}>
        <Form.Group className="mb-3" controlId="formBasicName">
        <Form.Label>Name</Form.Label>
        <Form.Control type="text" placeholder="Enter post name" name="name" />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicLocation">
        <Form.Label>Location</Form.Label>
        <Form.Control type="text" placeholder="Enter location" name="location" />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicDescription">
        <Form.Label>Description</Form.Label>
        <Form.Control as="textarea" placeholder="Enter the description here" name="description" />
        </Form.Group>
        <Row>
            <Col>
                <Form.Group className="mb-3" controlId="formBasicHourly">
                <Form.Label>Hourly price</Form.Label>
                <Form.Control type="number" placeholder="Enter the hourly price in HUF. Can be empty" name="hourly" />
                </Form.Group>
            </Col>
            <Col>
                <Form.Group className="mb-3" controlId="formBasicDaily">
                <Form.Label>Daily price</Form.Label>
                <Form.Control type="number" placeholder="Enter the daily price in HUF. Can be empty" name="daily" />
                </Form.Group>
            </Col>
            <Col>
                <Form.Group className="mb-3" controlId="formBasicWeekly">
                <Form.Label>Weekly price</Form.Label>
                <Form.Control type="number" placeholder="Enter the weekly price in HUF. Can be empty" name="weekly" />
                </Form.Group>
            </Col>

        </Row>
        <Form.Group className="mb-3" controlId="formBasicDeposit">
        <Form.Label>Security deposit</Form.Label>
        <Form.Control type="number" placeholder="Enter the secutiry deposit price in HUF. Can be empty" name="securityDeposit" />
        </Form.Group>
        {props.postType === PostType.Car.valueOf() ? (
        <>
        <Form.Group className="mb-3" controlId="formBasicSeats">
        <Form.Label>Number of seats</Form.Label>
        <Form.Control type="number" placeholder="Enter the number of seats" name="numberOfSeats" />
        </Form.Group>
        <Form.Check onChange={() => {canDeliver = !canDeliver}} className="d-flex flex-row-reverse justify-left justify-content-end mb-3" reverse type="switch" label="Can it be delivered to the customer?&nbsp;&nbsp;" name="canDeliverToYou"></Form.Check>
        </>
        ) : <>
        <Row>
            <Col>
                <Form.Group className="mb-3" controlId="formBasicWidth">
                <Form.Label>Width</Form.Label>
                <Form.Control type="number" placeholder="Enter the trailers's width in cm. Can be empty" name="width" />
                </Form.Group>
            </Col>
            <Col>
                <Form.Group className="mb-3" controlId="formBasicLength">
                <Form.Label>Length</Form.Label>
                <Form.Control type="number" placeholder="Enter the trailers's length in cm. Can be empty" name="length" />
                </Form.Group>
            </Col>
        </Row>
        </>}
        <Row className="mx-auto w-50 mb-3">
            <Button className="btn-dark" type="submit">Create post</Button>
        </Row>
    </Form></Container>)
}
export default CreatePostComponent;