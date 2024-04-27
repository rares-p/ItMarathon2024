import {Body, Controller, HttpCode, HttpException, HttpStatus, Put} from "@nestjs/common";
import {PreferenceService} from "./preference.service";
import {SetPreferenceDto} from "./dto/SetPreference.dto";

@Controller('preferences')
export class PreferenceController {
    constructor(
        private readonly preferenceService: PreferenceService
    ) {}

    @Put("")
    @HttpCode(HttpStatus.OK)
    async setPreferences(@Body() data: SetPreferenceDto) {
        const response = await this.preferenceService.setPreferences(
            data.studentId, data.preferences)

        if (response == undefined) {
            throw new HttpException("Unexpected error occurred", HttpStatus.INTERNAL_SERVER_ERROR);
        } else if (typeof response == "string") {
            throw new HttpException(response, HttpStatus.BAD_REQUEST);
        }

        return {};
    }
}