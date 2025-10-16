export interface User {
    id: string,
    fullName: string,
    email: string,
    gender: string,
    profilePictureURL: string,
    token: string
}

export interface LoginCredentials {
    userName: string,
    password: string
}

export interface RegisterCredentials {
    firstName: string,
    lastName: string,
    email: string,
    password: string,
    dateOfBirth: string,
    gender: string
}