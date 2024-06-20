import { Typography } from 'antd';
import { useTranslation } from 'react-i18next';

import { GradingMethod } from '../../models/course-material.type';
import { TestAttemptItem } from '../../models/test.interface';

interface Props {
  gradingMethod: GradingMethod;
  attempts: TestAttemptItem[];
}

export default function TestResult({ gradingMethod, attempts }: Props) {
  const { t } = useTranslation();
  const grades: number[] = [];

  for (const attemp of attempts) {
    if (attemp.grade) {
      grades.push(attemp.grade);
    }
  }

  if (!grades.length) {
    return null;
  }

  let result;

  switch (gradingMethod) {
    case GradingMethod.BestAttempt:
      result = Math.max(...grades);
      break;
    case GradingMethod.LastAttempt:
      result = grades[grades.length - 1];
      break;
    default:
      return null;
  }

  return (
    <Typography.Paragraph>
      {t('course_test.grade_for_test')}: {result}
    </Typography.Paragraph>
  );
}
