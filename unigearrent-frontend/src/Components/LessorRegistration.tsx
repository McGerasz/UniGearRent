import { Button, Col, Container, Form, Row } from "react-bootstrap"
import { Link } from "react-router-dom"
import PhoneNumberValidator from "../Utils/PhoneNumberValidator"
import { FormEvent, SyntheticEvent, useState } from "react"
import PasswordValidator from "../Utils/PasswordValidator"

const LessorRegistration: React.FC<{typeSelectedSetter: React.Dispatch<React.SetStateAction<boolean>>}> = (props) => {
    const [validPhoneNumber, setValidPhoneNumber] = useState<Boolean>(true)
    const [validPassword, setValidPassword] = useState<Boolean>(true)
    const SubmitHandler: (e: React.FormEvent) => void = (e) => {
        e.preventDefault();
        setValidPhoneNumber(true)
        setValidPassword(true)
        const target = e.target as typeof e.target & {
            email: {value: string};
            username: {value: string};
            phoneNumber: {value: string};
            firstname: {value: string};
            lastname: {value: string};
            password: {value: string};
        }
        let isValid: boolean = true;
        if(!PhoneNumberValidator(target.phoneNumber.value)) {
            setValidPhoneNumber(false);
            isValid = false
        }
        if(!PasswordValidator(target.password.value)){
            setValidPassword(false)
            isValid = false
        }
        if(!isValid) return;
        alert("Successful registration");
    }
    return(<Container className='w-50 mt-5 align-items-center'>
    <h1 className="text-center mb-5">Enter your registration details</h1>
    <Form onSubmit={SubmitHandler}>
        <Form.Group className="mb-3" controlId="formBasicEmail">
        <Form.Label>Email address</Form.Label>
        <Form.Control type="email" placeholder="Enter email" name="email" />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicUsername">
        <Form.Label>Username</Form.Label>
        <Form.Control type="text" placeholder="Enter username" name="username"/>
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicPhoneNumber">
        <Form.Label>Phone number</Form.Label>
        <Form.Control type="text" placeholder="Enter phone number" name="phoneNumber"/>
        </Form.Group>
        {validPhoneNumber ? <></> : <p style={{color:"red"}}>The phone number you provided was invalid!</p>}
        <Form.Group className="mb-3" controlId="formBasicName">
        <Form.Label>Name</Form.Label>
        <Form.Control type="text" placeholder="Enter your or your company's name" name="name"/>
        </Form.Group>
        <Form.Group className="mb-3 justify-content-md-center" controlId="formBasicPassword">
        <Form.Label>Password</Form.Label>
        <Form.Control type="password" placeholder="Password" name="password" />
        {validPassword ? <></> : <p style={{color:"red"}}>The password needs to contain at least 8 characters; including at least 1 uppercase letter, 1 lowercase letter and 1 number!</p>}
        </Form.Group>
        <Row className='w-75 mx-auto'>
            <Button className='btn btn-dark' variant="primary" type="submit">
                Register
            </Button>
        </Row>
        <Row className='mt-3 text-center'>
        </Row>
    </Form>
        <p onClick={() => props.typeSelectedSetter(false)} style={{cursor: "pointer"}}><u>Select a different account type</u></p>
  </Container>)
}
export default LessorRegistration