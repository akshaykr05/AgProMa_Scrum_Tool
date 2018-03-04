export class Sprint {
    constructor(
        public projectId: number,
        public sprintGoal: string,
        public sprintName: string,
        public totalDays: number,
        public startDate: Date,
        public status: number,
        public sprintId?: number,
        public releasePlanId?: number,
        public expectedEndDate?:Date,
        public actualEndDate?:Date
    ) { }
}