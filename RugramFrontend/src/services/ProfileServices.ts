import { ProfileRequest } from "../types/commonTypes";
import { ApiConnection } from "./ApiConnection";

class ProfileServices {
  static async Follow(id: string) {
    const response = await ApiConnection.put(`subscribe/${id}`);
    return response;
  }

  static async UnFollow(id: string) {
    const response = await ApiConnection.put(`unsubscribe/${id}`);
    return response;
  }

  static async GetProfile(id: string) {
    const name = await ApiConnection.get<{profileName: string}>(`profile/profileName/${id}`);
    const icon = `data:image/png;base64, ${(await ApiConnection.get<{photo: string}>(`profile/profilePhoto/${id}`)).data.photo}`;
    const followers = await ApiConnection.get<{
      subscribersCount: number,
      subscriptionsCount: number
    }>(`profile/profileIndicators/${id}`);

    return { ...name.data, icon , ...followers.data } as unknown as ProfileRequest;
  }
}

export default ProfileServices;
