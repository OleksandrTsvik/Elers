import { useCoursePermission } from '../../auth';
import { TestAttemptItem } from '../../models/test.interface';

interface Props {
  courseId: string | undefined;
  attempts: TestAttemptItem[];
  numberAttempts: number;
  children: React.ReactNode;
}

export default function StartTestGuard({
  courseId,
  attempts,
  numberAttempts,
  children,
}: Props) {
  const { isStudent } = useCoursePermission(courseId);

  if (
    !isStudent ||
    numberAttempts <= attempts.length ||
    attempts.some((item) => !item.isCompleted)
  ) {
    return null;
  }

  return children;
}
