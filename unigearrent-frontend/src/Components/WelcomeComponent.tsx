import { Container } from "react-bootstrap";

const WelcomeComponent: React.FC = () => {
    return(<Container className="mt-5">
        <h1 className="text-center mt-5" style={{fontSize:"100px"}}>Welcome to UniGearRent!</h1>
        <h2 className="text-center mt 5">The #1 place for renting cars and trailers!</h2>
    </Container>)
}
export default WelcomeComponent;