export class ProjectMaster {
    constructor(
        private name: string,
        private leaderId: number,
        private projectDescription: string,
        private technologyUsed: string,
        private Id?: number,
        private actAs?: string
    )
    { }
}