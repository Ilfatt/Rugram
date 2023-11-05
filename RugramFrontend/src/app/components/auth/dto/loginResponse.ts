export class LoginResponse {
    constructor(public refreshToken: string, public jwtToken: string) {
    }
}
