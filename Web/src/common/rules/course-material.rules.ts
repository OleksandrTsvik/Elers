import {
  MAX_ASSIGNMENT_GRADE,
  MAX_FILES_STUDENT_UPLOAD_ASSIGNMENT,
} from '../../utils/constants/app.constants';

export const COURSE_MATERIAL_RULES = {
  link: {
    title: { min: 2, max: 64 },
    link: { max: 2048 },
  },
  file: {
    title: { min: 2, max: 64 },
  },
  assignment: {
    title: { min: 2, max: 64 },
    maxFiles: { min: 0, max: MAX_FILES_STUDENT_UPLOAD_ASSIGNMENT },
    maxGrade: { min: 1, max: MAX_ASSIGNMENT_GRADE },
  },
  test: {
    title: { min: 2, max: 64 },
    numberAttempts: { min: 1, max: 100 },
    timeLimitInMinutes: { min: 1, max: 43_200 },
  },
};
