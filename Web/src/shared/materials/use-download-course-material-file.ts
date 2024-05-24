import { DOWNLOAD_COURSE_MATERIAL_FILE_LINK } from './course-materials.constants';
import useDownloadFile from '../../hooks/use-download-file';

export function useDownloadCourseMaterialFile() {
  const { downloadFile } = useDownloadFile();

  const downloadCourseMaterialFile = async (
    uniqueFileName: string,
    fileName: string,
  ) => {
    await downloadFile(
      DOWNLOAD_COURSE_MATERIAL_FILE_LINK + uniqueFileName,
      fileName,
    );
  };

  return { downloadCourseMaterialFile };
}
