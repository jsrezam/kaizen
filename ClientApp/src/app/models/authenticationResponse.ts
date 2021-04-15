export interface AuthenticationResponse {
    token?: string
    expiration?: Date
    errorMessage?: string
    errors?: any[]
}