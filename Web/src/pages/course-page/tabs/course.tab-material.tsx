import { Button, Space } from 'antd';
import { Link } from 'react-router-dom';

import { TextEditorOutput } from '../../../common/typography';
import { CourseMaterial } from '../../../models/course-material.type';
import {
  CourseMaterialFileIcon,
  CourseMaterialIcon,
  CourseMaterialType,
  useDownloadCourseMaterialFile,
} from '../../../shared';

interface Props {
  material: CourseMaterial;
}

export default function CourseTabMaterial({ material }: Props) {
  const { downloadCourseMaterialFile } = useDownloadCourseMaterialFile();

  switch (material.type) {
    case CourseMaterialType.Content:
      return <TextEditorOutput text={material.content} />;
    case CourseMaterialType.Link:
      return (
        <Link className="d-block" to={material.link} target="_blank">
          <Space align="start">
            <CourseMaterialIcon type={CourseMaterialType.Link} />
            {material.title}
          </Space>
        </Link>
      );
    case CourseMaterialType.File:
      return (
        <Button
          className="d-block p-0"
          type="link"
          icon={<CourseMaterialFileIcon extension={material.fileName} />}
          onClick={() =>
            downloadCourseMaterialFile(
              material.uniqueFileName,
              material.fileName,
            )
          }
        >
          {material.title}
        </Button>
      );
  }
}
