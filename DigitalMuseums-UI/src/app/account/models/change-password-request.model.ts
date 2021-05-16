export interface ChangePasswordRequest {
    oldPassword: string;
    newPassword: string;
    newPasswordConfirmation: string;
}