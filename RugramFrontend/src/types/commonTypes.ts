export interface JwtResponseType {
  jwtToken: "string";
  refreshToken: "string";
}

export type WithValidation = {
  validate(): string | undefined;
};

export type ProfileRequest = {
  profileName: string,
  icon: string,
  subscribersCount: number,
  subscriptionsCount: number
}

export type User = {
  id?: string,
  username?: string,
  img?: string,
  description?: string,
  followersCount?: number,
  followingCount?: number,
  posts?: {
    id: string,
    img: string,
    postDescription: string,
  }
}