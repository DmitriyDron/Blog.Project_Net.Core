export class RegisterInput {
    username: string;
    email : string;
    password: string;
    confirmPassword: string;

    checkInputs(): boolean {
        if (this.username === undefined || this.email === undefined || this.password === undefined) {
            return false;
        }
        return true;
    }

    checkPassword(): boolean {
        if (this.password !== this.confirmPassword) {
            return false;
        }
        return true;
    }
}