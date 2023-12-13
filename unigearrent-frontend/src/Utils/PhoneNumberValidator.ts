export default function PhoneNumberValidator(input: string): boolean{
    [...input].forEach((char) => {
        if(isNaN(Number(char)) && char !== "+" && char !== " " && char !== "-") return false;
    })
    return true;
}