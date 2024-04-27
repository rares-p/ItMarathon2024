import {IsNotEmpty, IsString, IsUUID} from "class-validator";
import {UUID} from "../../Utils/Types";

export class GetAllUsersDto {
    @IsNotEmpty()
    @IsUUID()
    userId: UUID;
}
