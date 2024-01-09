import { Container } from "react-bootstrap"
import RegisterComponent from "../Components/RegisterComponent"
import { useState } from "react"

const RegisterPage: React.FC = () => {
    return(<Container className="justify-content-md-center">
        <RegisterComponent />
    </Container>)
}
export default RegisterPage