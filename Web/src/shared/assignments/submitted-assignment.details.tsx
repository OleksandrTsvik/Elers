import { Button, Typography } from 'antd';
import dayjs from 'dayjs';
import { useTranslation } from 'react-i18next';

import { SubmittedAssignmentStatusParagraph } from './submitted-assignment-status-paragraph';
import { TextEditorOutput } from '../../common/typography';
import useDownloadFile from '../../hooks/use-download-file';
import {
  SubmittedAssignment,
  SubmittedAssignmentStatus,
} from '../../models/assignment.interface';
import { DATE_FORMAT } from '../../utils/constants/app.constants';
import { isNumber } from '../../utils/helpers';
import { CourseMaterialFileIcon } from '../materials';

interface Props {
  submittedAssignment: SubmittedAssignment;
}

export function SubmittedAssignmentDetails({
  submittedAssignment: {
    status,
    attemptNumber,
    grade,
    teacher,
    text,
    files,
    submittedAt,
    teacherComment,
  },
}: Props) {
  const { t } = useTranslation();
  const { downloadFileOrOpenPdf } = useDownloadFile();

  return (
    <>
      <Typography.Title level={3}>
        {t('assignment.last_uploaded_answer')}
      </Typography.Title>

      <SubmittedAssignmentStatusParagraph status={status} />

      <Typography.Paragraph>
        {t('assignment.attempt_number')} №: {attemptNumber}
      </Typography.Paragraph>

      {(attemptNumber > 1 || status !== SubmittedAssignmentStatus.Submitted) &&
        isNumber(grade) && (
          <Typography.Paragraph>
            {t('assignment.grade')}: {grade}
          </Typography.Paragraph>
        )}

      {teacherComment && (
        <Typography.Paragraph>
          {t('assignment.comment')}: {teacherComment}
        </Typography.Paragraph>
      )}

      {teacher && (
        <Typography.Paragraph>
          {t('assignment.teacher')}: {teacher.lastName} {teacher.firstName}{' '}
          {teacher.patronymic}
        </Typography.Paragraph>
      )}

      {text && <TextEditorOutput text={text} />}

      {files.map((file) => (
        <Button
          key={file.uniqueFileName}
          className="d-block p-0"
          type="link"
          icon={<CourseMaterialFileIcon extension={file.fileName} />}
          onClick={() =>
            downloadFileOrOpenPdf(
              '/assignments/file/download/' + file.uniqueFileName,
              file.fileName,
            )
          }
        >
          {file.fileName}
        </Button>
      ))}

      <Typography.Paragraph>
        {t('assignment.submitted_at')}: {dayjs(submittedAt).format(DATE_FORMAT)}
      </Typography.Paragraph>
    </>
  );
}
