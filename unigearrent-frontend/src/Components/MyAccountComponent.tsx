import { Button, Col, Container, Form, Row } from "react-bootstrap"
import { UserProfile } from "../Models/UserProfile"
import { useUserProfile } from "../Utils/UserProfileContextProvider"
import { RegistrationType } from "../Models/RegistrationType";
import { useState } from "react";
import PhoneNumberValidator from "../Utils/PhoneNumberValidator";
import BackendURL from "../Utils/BackendURL";
import { useNavigate } from "react-router-dom";
import Cookies from "universal-cookie";

const MyAccountComponent: React.FC<{profileData: any}> = (props) => {
    const navigate = useNavigate();
    const profile = useUserProfile().userProfile;
    const profileSetter = useUserProfile().setUserProfile;
    const [validPhoneNumber, setValidPhoneNumber] = useState<Boolean>(true)
    const EditHandler: (e: React.FormEvent) => void = async (e) => {
        e.preventDefault();
        setValidPhoneNumber(true)
        if(profile?.Type === RegistrationType.Lessor){
        const target = e.target as typeof e.target & {
            email: {value: string};
            username: {value: string};
            phonenumber: {value: string};
            name: {value: string};
        }
        let isValid: boolean = true;
        if(!PhoneNumberValidator(target.phonenumber.value)) {
            setValidPhoneNumber(false);
            isValid = false
        }
        if(!isValid) return;
        const data = {
            id: profile?.Id,
            email: target.email.value,
            username: target.username.value,
            phonenumber: target.phonenumber.value,
            name: target.name.value
        }
        profile.Email = data.email;
        profile.PhoneNumber = data.phonenumber;
        profile.Username = data.username;
        profileSetter(profile); 
        await fetch(BackendURL + "Post/lessor", {
            method: "PUT",
            headers: {
                'Content-Type': 'application/json',
            },
            body:JSON.stringify(data)
        });
        alert("Successfully updated profile");
        navigate("/");
        return;
        }
        const target = e.target as typeof e.target & {
            email: {value: string};
            username: {value: string};
            phonenumber: {value: string};
            firstName: {value: string};
            lastName: {value: string};
        }
        let isValid: boolean = true;
        if(!PhoneNumberValidator(target.phonenumber.value)) {
            setValidPhoneNumber(false);
            isValid = false
        }
        if(!isValid) return;
        const data = {
            id: profile?.Id,
            email: target.email.value,
            username: target.username.value,
            phonenumber: target.phonenumber.value,
            firstName: target.firstName.value,
            lastName: target.lastName.value
        }
        if(profile != null){
            profile.Email = data.email;
            profile.PhoneNumber = data.phonenumber;
            profile.Username = data.username;
            profileSetter(profile);
        }
        await fetch(BackendURL + "Post/user", {
            method: "PUT",
            headers: {
                'Content-Type': 'application/json',
            },
            body:JSON.stringify(data)
        });
        alert("Successfully updated profile");
        navigate("/");
    }
    const DeleteHandler: () => void = async () => {
        await fetch(BackendURL + "Post/profile/" + profile?.Id, {
            method: "DELETE"
        });
        profileSetter(null);
        let cookies = new Cookies();
        cookies.remove("profile");
        alert("Account successfully deleted");
        navigate("/");
    }
    return <Container>
        <Form onSubmit={EditHandler} className="mt-3">
            <Form.Group className="mb-3" controlId="formBasicEmail">
            <Form.Label>Email address</Form.Label>
            <Form.Control type="email" placeholder="Enter email" name="email" defaultValue={profile?.Email}/>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formBasicUsername">
            <Form.Label>Username</Form.Label>
            <Form.Control type="text" placeholder="Enter username" name="username" defaultValue={profile?.Username}/>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formBasicPhoneNumber">
            <Form.Label>Phonenumber</Form.Label>
            <Form.Control type="text" placeholder="Enter phonenumber" name="phonenumber" defaultValue={profile?.PhoneNumber}/>
            </Form.Group>
            {profile?.Type === RegistrationType.Lessor ? (<>
                <Form.Group className="mb-3" controlId="formBasicName">
                <Form.Label>Name</Form.Label>
                <Form.Control type="text" placeholder="Enter Name" name="name" defaultValue={props.profileData["name"]}/>
                </Form.Group>
        </>) : (<>
                <Form.Group className="mb-3" controlId="formBasicFirstName">
                <Form.Label>First name</Form.Label>
                <Form.Control type="text" placeholder="Enter first name" name="firstName" defaultValue={props.profileData["firstName"]}/>
                </Form.Group>
                <Form.Group className="mb-3" controlId="formBasicLastName">
                <Form.Label>Last name</Form.Label>
                <Form.Control type="text" placeholder="Enter last name" name="lastName" defaultValue={props.profileData["lastName"]}/>
                </Form.Group>
        </>)}
            <Row>
                <Col><Button type="submit">Edit account</Button></Col>
                <Col><Button variant="danger" onClick={DeleteHandler}>Delete account</Button></Col>
            </Row>
        </Form>
    </Container>
}
export default MyAccountComponent;