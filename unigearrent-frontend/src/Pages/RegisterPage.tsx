import { Container } from "react-bootstrap"
import RegisterComponent from "../Components/RegisterComponent"
import { useState } from "react"

const RegisterPage: React.FC = () => {
    const [typeSelected, setTypeSelected] = useState(false)
    return(<Container className="justify-content-md-center">
        <RegisterComponent />
    </Container>)
}
export default RegisterPage