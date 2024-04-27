import {UUID} from "../../Utils/Types";
import {IsNotEmpty, IsString} from "class-validator";

export class GetAllIdentifiersDto {
    @IsNotEmpty()
    @IsString()
    userId: UUID
}